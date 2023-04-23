using SAP_API.Exceptions;
using System;
using System.Collections.Generic;
using static SAP_API.Services.ArrangingProductsToProgramsService;

namespace SAP_API.Models
{
    public class BakingProgram: IEntity
    {
        
        public Guid Id { get; set; }
        public string Code { get; set; }
        public DateTime CreatedAt { get; set; }
        public BakingProgramStatus Status { get; set; }
        public int BakingTimeInMins { get; set; }
        public int BakingTempInC { get; set; }
        public DateTime BakingProgrammedAt { get; set; }
        public DateTime? BakingStartedAt { get; set; }
        public DateTime? BakingEndsAt { get; set; }
        public Guid OvenId { get; set; }

        public Oven Oven { get; set; }
        public Guid? PreparedByUserId { get; set; }
        public User PreparedByUser { get; set; }
        public int RemainingOvenCapacity { get; set; }
        public List<BakingProgramProduct> Products { get; set; }

        public void AddProductToProgram(BakingProgramProduct product)
        {
            Products.Add(product);
            int productSize = product.Product.Size;
            int productQuantity = product.QuantityТoBake;
            RemainingOvenCapacity -= productSize * productQuantity;
        }

        internal void SetTimeAndTemp(TimeAndTempGroup group)
        {
            BakingTempInC = group.Temp;
            BakingTimeInMins = group.Time;
        }

        //TODO PreparedBy
        internal void StartPreparing(Guid userId)
        {
            if (!Status.Equals(BakingProgramStatus.Created))
            {
                string message = CreateUnableToTransitionErrorMessage(BakingProgramStatus.Preparing);
                throw new BadProgramStatusException(message);
            }
                
            Status = BakingProgramStatus.Preparing;
            PreparedByUserId = userId;
        }

        private string CreateUnableToTransitionErrorMessage(BakingProgramStatus statusToTransitionTo)
        {
            return "Cannot transition to status " + Enum.GetName(typeof(BakingProgramStatus), statusToTransitionTo) + " from status " + Enum.GetName(typeof(BakingProgramStatus), Status);
        }

        internal void FinishPreparing()
        {
            if (!Status.Equals(BakingProgramStatus.Preparing))
            {
                string message = CreateUnableToTransitionErrorMessage(BakingProgramStatus.Prepared);
                throw new BadProgramStatusException(message);
            }
            Status = BakingProgramStatus.Prepared;
        }

        internal void CancellPreparing()
        {
            if (!Status.Equals(BakingProgramStatus.Preparing))
            {
                string message = CreateUnableToTransitionErrorMessage(BakingProgramStatus.Created);
                throw new BadProgramStatusException(message);
            }
            Status = BakingProgramStatus.Created;
            PreparedByUser = null;
        }

        //TODO minutes as params
        internal DateTime GetTimeProgramCanBePreparedAt()
        {
            return BakingProgrammedAt.AddMinutes(-25);
        }

        internal DateTime GetTimeProgramCanBeBakedAt()
        {
            return BakingProgrammedAt.AddMinutes(-5);
        }

        internal void StartBaking()
        {
            if (!Status.Equals(BakingProgramStatus.Prepared))
            {
                string message = CreateUnableToTransitionErrorMessage(BakingProgramStatus.Baking);
                throw new BadProgramStatusException(message);
            }
            Status = BakingProgramStatus.Baking;
            BakingStartedAt = DateTime.Now;
            BakingEndsAt = BakingStartedAt?.AddMinutes(BakingTimeInMins);
        }

        internal bool IsBakingDone()
        {
            return BakingEndsAt != null && DateTime.Compare((DateTime)BakingEndsAt, DateTime.Now) <= 0;
        }

        internal void FinishBaking()
        {
            if (!Status.Equals(BakingProgramStatus.Baking))
            {
                string message = CreateUnableToTransitionErrorMessage(BakingProgramStatus.Done);
                throw new BadProgramStatusException(message);
            }
            Status = BakingProgramStatus.Done;
        }

        internal void Finish()
        {
            if (!Status.Equals(BakingProgramStatus.Done))
            {
                string message = CreateUnableToTransitionErrorMessage(BakingProgramStatus.Finished);
                throw new BadProgramStatusException(message);
            }
            Status = BakingProgramStatus.Finished;
        }
    }
}
