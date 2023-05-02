import ProductToPrepareResponse from "./ProductToPrepareResponse";

export default interface LocationToPrepareFromResponse {
    locationId: string;
    locationCode: string;
    products: ProductToPrepareResponse[];
}
