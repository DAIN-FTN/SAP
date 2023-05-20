import { useEffect, useState } from "react";
import { FC } from "react";
import styled from "styled-components";
import PrepareForOvenList from "./PrepareForOvenList";
import { getAll } from "../../services/BakingProgramService";
import AllBakingPrograms from "../../models/Responses/AllBakingProgramsResponse";
import PreparingList from "./PreparingList";
import BakingList from "./BakingList";
import DoneList from "./DoneList";
import { BakingProgramStatus } from "../../models/Enums/BakingProgramStatus";

const Container = styled.div`
    width: 100%;
    padding: 36px;
    display: flex;
    flex-direction: row;
    -webkit-box-shadow: 0px 2px 14px 0px rgba(122,122,122,1);
    -moz-box-shadow: 0px 2px 14px 0px rgba(122,122,122,1);
    box-shadow: 0px 2px 14px 0px rgba(122,122,122,1);
    background-color: white;
    margin: 48px;
    flex-wrap: wrap;
    align-content: flex-start;
`;

const Panel = styled.div`
    width: 50%;
    min-width: 50%;
    padding: 24px;
    display: flex;
    flex-direction: column;
    box-sizing: border-box;
`;

const Label = styled.p`
    font-size: 24px;
`;

const ErrorMessage = styled.p`
    font-style: italic;
`;

const HomePage: FC = () => {
    const [allBakingPrograms, setAllBakingPrograms] = useState<AllBakingPrograms | null>(null);

    useEffect(() => {
        console.log('useEffect() in HomePage');
        getAll().then((allBakingPrograms) => { 
            console.log('fetch finished for fetchAllBakingPrograms() in HomePage');
            const alBakingPrograms = {
                prepareForOven: [
                ],
                preparingAndPrepared: [
                    // {
                    //     id: "adef3240-0efa-4efc-a232-589de8b91f39",
                    //     code: "Oven 1",
                    //     createdAt: new Date(),
                    //     status: BakingProgramStatus.Prepared,
                    //     bakingTimeInMins: 10,
                    //     bakingTempInC: 270,
                    //     bakingProgrammedAt: new Date("2023-05-20T01:09:55"),
                    //     canBePreparedAt: new Date("2023-05-20T01:09:55"),
                    //     canBeBakedAt: new Date("2023-05-20T01:09:55"),
                    //     bakingStartedAt: new Date("2023-05-20T01:09:55"),
                    //     ovenId: "adef3240-0efa-4efc-a232-589de8b91f39",
                    //     ovenCode: "Oven 1"
                    // }
                ],
                preparingInProgress: null,
                // {
                //     id: "adef3240-0efa-4efc-a232-589de8b91f39",
                //     code: "Oven 1",
                //     bakingTimeInMins: 10,
                //     bakingTempInC: 270,
                //     bakingProgrammedAt: new Date("2023-05-20T01:09:55"),
                //     ovenId: "adef3240-0efa-4efc-a232-589de8b91f39",
                //     ovenCode: "Oven 1",
                //     locations: []
                // },
                baking: [
                    // {
                    //     id: "adef3240-0efa-4efc-a232-589de8b91f39",
                    //     code: "Oven 1",
                    //     createdAt: new Date(),
                    //     status: BakingProgramStatus.Baking,
                    //     bakingTimeInMins: 10,
                    //     bakingTempInC: 270,
                    //     bakingProgrammedAt: new Date("2023-05-20T01:09:55"),
                    //     canBePreparedAt: new Date("2023-05-20T01:09:55"),
                    //     canBeBakedAt: new Date("2023-05-20T01:09:55"),
                    //     bakingStartedAt: new Date("2023-05-20T02:09:55"),
                    //     ovenId: "adef3240-0efa-4efc-a232-589de8b91f39",
                    //     ovenCode: "Oven 1"
                    // }
                    // 
                ],
                done: [
                    // {
                    //     id: "adef3240-0efa-4efc-a232-589de8b91f39",
                    //     code: "Oven 1",
                    //     createdAt: new Date(),
                    //     status: BakingProgramStatus.Baking,
                    //     bakingTimeInMins: 10,
                    //     bakingTempInC: 270,
                    //     bakingProgrammedAt: new Date("2023-05-20T01:09:55"),
                    //     canBePreparedAt: new Date("2023-05-20T01:09:55"),
                    //     canBeBakedAt: new Date("2023-05-20T01:09:55"),
                    //     bakingStartedAt: new Date("2023-05-20T02:09:55"),
                    //     ovenId: "adef3240-0efa-4efc-a232-589de8b91f39",
                    //     ovenCode: "Oven 1"
                    // }
                ]
            }
            setAllBakingPrograms(allBakingPrograms);
        });
    }, []);

    function refreshView () {
        getAll().then((allBakingPrograms) => { 
            console.log('fetch finished for fetchAllBakingPrograms() in HomePage');
            setAllBakingPrograms(allBakingPrograms);
        });
    }

    if (allBakingPrograms === undefined || allBakingPrograms === null) {
        return (
            <Container>
                {/* <ErrorMessage>No baking programs available</ErrorMessage> */}
            </Container>
        );
    }

    return (
        <Container>
            <Panel>
                <Label>Prepare for oven</Label>
                <PrepareForOvenList props={{prepareForOven: allBakingPrograms?.prepareForOven, refreshView}} />
            </Panel>
            <Panel>
                <Label>Preparing</Label>
                <PreparingList props={{preparingBakingPrograms: allBakingPrograms?.preparingAndPrepared, preparingInProgress: allBakingPrograms.preparingInProgress, refreshView}} />
            </Panel>
            <Panel>
                <Label>Baking</Label>
                <BakingList props={{bakingBakingPrograms: allBakingPrograms?.baking, refreshView}} />
            </Panel>
            <Panel>
                <Label>Done</Label>
                <DoneList props={{doneBakingPrograms: allBakingPrograms?.done, refreshView}} />
            </Panel>
        </Container>
    );
};

export default HomePage;
