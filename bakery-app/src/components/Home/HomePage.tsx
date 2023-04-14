import { useEffect, useState } from "react";
import { FC } from "react";
import styled from "styled-components";
import PrepareForOvenList from "./PrepareForOvenList";
import { fetchAllBakingPrograms } from "../../services/BakingProgramService";
import { AllBakingPrograms } from "../../models/AllBakingPrograms";

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

const SearchWrapper = styled.div`
    display: flex;
    flex-direction: row;
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

    if (allBakingPrograms == undefined || allBakingPrograms == null) {
        return <p>No baking programs available</p>;
    }

    return (
        <Container>
            <Panel>
                <Label>Prepare for oven</Label>
                <PrepareForOvenList props={{prepareForOven: allBakingPrograms?.prepareForOven!}} />
            </Panel>
            <Panel>
                <Label>Preparing</Label>
                <PrepareForOvenList props={{prepareForOven: allBakingPrograms?.preparingAndPrepared!}} />
            </Panel>
            <Panel>
                <Label>Baking</Label>
                <PrepareForOvenList props={{prepareForOven: allBakingPrograms?.baking!}} />
            </Panel>
            <Panel>
                <Label>Done</Label>
                <PrepareForOvenList props={{prepareForOven: allBakingPrograms?.done!}} />
            </Panel>
        </Container>
    );
};

export default HomePage;
