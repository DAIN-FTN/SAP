import { getData, postData } from "./DataService";
import { BakingTimeSlot as BakingProgram } from "../models/BakingTimeSlot";
import { NewOrderRequest } from "../models/NewOrderRequest";
import { Order } from "../models/Order";
import { ProductBasicInfo } from "../models/ProductBasicInfo";
import { AllBakingPrograms } from "../models/AllBakingPrograms";

export async function fetchAllBakingPrograms(): Promise<AllBakingPrograms> {
    try {
        const response = await getData<AllBakingPrograms>(`/api/baking-programs`);

        return mapAllBakingPrograms(response);
    } catch (error) {
        throw new Error("");
    }
}

// export async function fetchBakingTimeSlots(deliveryDateTime: Date, products: ProductBasicInfo[]): Promise<BakingProgram[]> {
//     const bakingTimeSlotRequest = {
//         shouldBeDoneAt: deliveryDateTime.toISOString(),
//         orderProducts: products.map((product) => ({
//             productId: product.id,
//             quantity: product.quantity,
//         }))
//     };
//     console.log(bakingTimeSlotRequest);

//     const temp = await postData<BakingProgram[]>(`/api/baking-programs/available`, bakingTimeSlotRequest);

//     return temp.map((bakingTimeSlot) => ({
//         ...bakingTimeSlot,
//         bakingStartedAt: new Date(bakingTimeSlot.bakingStartedAt),
//         bakingProgrammedAt: new Date(bakingTimeSlot.bakingProgrammedAt),
//         createdAt: new Date(bakingTimeSlot.createdAt)
//     }));
// }

// export async function createNewOrder(name: string, newOrderRequest: NewOrderRequest): Promise<Order> {
//     return await postData<Order>(`/api/products/stock?name=${name}`, newOrderRequest);
// }

function mapAllBakingPrograms(allBakingPrograms: AllBakingPrograms): AllBakingPrograms {
    return {
        preparingInProgress: allBakingPrograms.preparingInProgress,
        prepareForOven: mapBakingPrograms(allBakingPrograms.prepareForOven),
        preparingAndPrepared: mapBakingPrograms(allBakingPrograms.preparingAndPrepared),
        baking: mapBakingPrograms(allBakingPrograms.baking),
        done: mapBakingPrograms(allBakingPrograms.done),
    }

    function mapBakingPrograms(bakingPrograms: BakingProgram[]): BakingProgram[] {
        return bakingPrograms.map((bakingProgram) => ({
            ...bakingProgram,
            bakingStartedAt: new Date(bakingProgram.bakingStartedAt),
            bakingProgrammedAt: new Date(bakingProgram.bakingProgrammedAt),
            createdAt: new Date(bakingProgram.createdAt)
        }))
    }
}