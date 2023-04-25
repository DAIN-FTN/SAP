import { useEffect, useState } from "react";
import { FC } from "react";
import styled from "styled-components";
import PrepareForOvenList from "./PrepareForOvenList";
import { fetchAllBakingPrograms } from "../../services/BakingProgramService";
import AllBakingPrograms from "../../models/Responses/AllBakingProgramsResponse";
import PreparingList from "./PreparingList";
import BakingList from "./BakingList";
import DoneList from "./DoneList";

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
        fetchAllBakingPrograms().then((allBakingPrograms) => { 
            console.log('fetch finished for fetchAllBakingPrograms() in HomePage');
            setAllBakingPrograms(allBakingPrograms);
        });
    }, []);

    function refreshView () {
        fetchAllBakingPrograms().then((allBakingPrograms) => { 
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
