import { useEffect, useState } from "react";
import { FC } from "react";
import styled from "styled-components";
import TextField from "@mui/material/TextField";
import CreateStockedProductRequest from "../../../../models/Requests/StockedProducts/CreateStockedProductRequest";
import { getAll } from "../../../../services/StockLocationService";
import StockLocationResponse from "../../../../models/Responses/StockLocation/StockLocationResponse";
import FormControl from "@mui/material/FormControl";
import InputLabel from "@mui/material/InputLabel";
import Select, { SelectChangeEvent } from "@mui/material/Select";
import MenuItem from "@mui/material/MenuItem";
import Table from "@mui/material/Table";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import TableCell from "@mui/material/TableCell";
import TableBody from "@mui/material/TableBody";

export interface StockFormProps {
    setStock: (stock: CreateStockedProductRequest[] | null) => void;
}

const Label = styled.p`
    font-size: 24px;
`;

const TableRowStyled = styled(TableRow)`
    cursor: pointer;
    &:hover {
        background-color: #f5f5f5;
    }
`;

const PrepareButtonWrapper = styled.div`
    float: right;
`;

const PrepareButton = styled.div`
    cursor: pointer;
    background-color: #DC3F3F;
    border: none;
    color: #fff;
    box-sizing: border-box;
    transition: background-color 0.1s ease-in-out;
    width: fit-content;
    padding: 0px 14px;
    border-radius: 21px;
    margin-left: 4px;

    &:hover {
        background-color: #cc2a2a;
    }
`;

const StockForm: FC<StockFormProps> = ({ setStock }) => {
    const [stockLocations, setStockLocations] = useState<StockLocationResponse[]>([]);
    const [stockProducts, setStockProducts] = useState<CreateStockedProductRequest[]>([]);
    const [newLocationId, setNewLocationId] = useState<string>();
    const [newQuantity, setNewQuantity] = useState<number>();

    useEffect(() => {
        getAll().then((stockLocations: StockLocationResponse[]) => {
            setStockLocations(stockLocations);
            setNewLocationId(stockLocations[0].id);
        });
    }, []);

    function addNewStockLocation() {
        if (!newLocationId || !newQuantity) return;

        const newStockLocation: CreateStockedProductRequest = {
            productId: null,
            locationId: newLocationId,
            quantity: newQuantity
        };

        setStockProducts([...stockProducts, newStockLocation]);

        const temp = [...stockProducts, newStockLocation];

        if (temp.length === 0) {
            setStock(null);
        } else {
            setStock([...stockProducts, newStockLocation]);
        }

        // setStockLocations(stockLocations.filter((stockLocation: StockLocationResponse) => {
        //     return stockLocation.id !== newLocationId;
        // }));

        // setNewLocationId(stockLocations[0].id);
    }

    function handleChange(event: SelectChangeEvent) {
        setNewLocationId(event.target.value);
    };

    function getLocationCode(locationId: string) {
        // debugger;

        const location = stockLocations.find((stockLocation: StockLocationResponse) => {
            return stockLocation.id === locationId;
        });

        return location?.code;
    }

    function getStockLocationsForDropdown() {
        return stockLocations.filter((stockLocation: StockLocationResponse) => {
            return !stockProducts.some((stockProduct: CreateStockedProductRequest) => {
                return stockProduct.locationId === stockLocation.id;
            });
        });
    }

    function removeStockProduct(locationId: string) {
        setStockProducts(stockProducts.filter((stockProduct: CreateStockedProductRequest) => {
            return stockProduct.locationId !== locationId;
        }));

        setStock(stockProducts.filter((stockProduct: CreateStockedProductRequest) => {
            return stockProduct.locationId !== locationId;
        }));
    }

    return (
        <>
            <Label>Stock locations</Label>
            <Table sx={{ minWidth: 150 }} size="small" aria-label="simple table">
                <TableHead>
                    <TableRow>
                        <TableCell>Location code</TableCell>
                        <TableCell align="right">Quantity</TableCell>
                        <TableCell align="right">Action</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {stockProducts.map((stockProduct) => (
                        <TableRowStyled key={stockProduct.locationId} sx={{ '&:last-child td, &:last-child th': { border: 0 } }}>
                            <TableCell component="th" scope="row">{getLocationCode(stockProduct.locationId)}</TableCell>
                            <TableCell align="right">{stockProduct.quantity}</TableCell>
                            <TableCell align="right">
                                <PrepareButtonWrapper>
                                    <PrepareButton onClick={() => removeStockProduct(stockProduct.locationId)}>Remove</PrepareButton>
                                </PrepareButtonWrapper>
                            </TableCell>
                        </TableRowStyled>
                    ))}
                    {getStockLocationsForDropdown().length > 0 && <TableRowStyled >
                        <TableCell component="th" scope="row">
                            <FormControl variant="standard" sx={{ m: 1, minWidth: 120 }}>
                                <InputLabel id="demo-simple-select-standard-label">Location code</InputLabel>
                                <Select
                                    labelId="demo-simple-select-standard-label"
                                    id="demo-simple-select-standard"
                                    value={newLocationId || ""}
                                    onChange={handleChange}
                                    label="Age"
                                >
                                    {getStockLocationsForDropdown().map((stockLocation: StockLocationResponse) => {
                                        return (
                                            <MenuItem key={stockLocation.id} value={stockLocation.id}>{stockLocation.code}</MenuItem>
                                        );
                                    })}
                                </Select>
                            </FormControl>
                        </TableCell>
                        <TableCell align="right">
                            <TextField id="standard-basic4" label="Size" variant="standard" sx={{ width: '40px' }} onChange={(e) => setNewQuantity(parseInt(e.target.value))} />
                        </TableCell>
                        <TableCell align="right">
                            <PrepareButtonWrapper>
                                <PrepareButton onClick={addNewStockLocation}>Add</PrepareButton>
                            </PrepareButtonWrapper>
                        </TableCell>
                    </TableRowStyled>}
                </TableBody>
            </Table>
        </>
    );
};

export default StockForm;
