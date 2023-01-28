import React from "react";
import { FC } from "react";
import styled from "styled-components";
import { ProductBasicInfo } from "../models/ProductBasicInfo";
// import Table from 'react-bootstrap/Table';
// import 'bootstrap/dist/css/bootstrap.min.css';

const Container = styled.div`
`;

const Table = styled.table`
    width: 100%;
    font-size: 20px;
`;

const AdditionContainer = styled.div`
    display: flex;
    flex-direction: row;
`;


const PlusMinusButton = styled.span`
    width: 24px;
    height: 24px;
    border-radius: 15px;
    align-items: center;
    justify-content: center;
    &:hover {
        cursor: pointer;
    }
`;

const MinusButton = styled(PlusMinusButton)`
    background-color: red;
`;

const PlusButton = styled(PlusMinusButton)`
    background-color: green;
`;

export interface AvailableProductsListProps {
    products: ProductBasicInfo[];
}

const AvailableProductsList: FC<{ props: AvailableProductsListProps }> = ({ props }) => {
    return (
        <Container>
            <Table>
                <thead>
                    <tr>
                        <td>Product name</td>
                        <td>Available quantity</td>
                        <td>For this order</td>
                    </tr>
                </thead>
                <tbody>
                    {props.products.map((product) => (
                        <tr key={product.id}>
                            <td>{product.name}</td>
                            <td>{product.availableQuantity}</td>
                            <td>
                                <AdditionContainer>
                                    <MinusButton>-</MinusButton>
                                    <span>0</span>
                                    <PlusButton>+</PlusButton>
                                </AdditionContainer>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </Table>
        </Container>
    );
};

export default AvailableProductsList;
