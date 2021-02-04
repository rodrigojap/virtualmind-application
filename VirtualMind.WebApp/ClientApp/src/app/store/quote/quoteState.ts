import { quote } from '../../models/quote.model';

export default class QuoteState {
    dolar: quote;
    real: quote;
}

let initial: quote = {
    lastUpdate: "",
    purchase: "0",
    sale: "0"
}

export const initializeState = (): QuoteState => {
    return { dolar: initial, real: initial };
};