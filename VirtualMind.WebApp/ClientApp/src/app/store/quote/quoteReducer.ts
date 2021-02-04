import { Action, createReducer, on } from '@ngrx/store';
import * as QuoteActions from './quoteAction';
import { quote } from '../../models/quote.model';
import QuoteState, { initializeState } from './quoteState';

export const intialState = initializeState();

export const reducer = createReducer(
    intialState,
    on(QuoteActions.getDolar, (state, action) => ({
        ...state,
        dolar: action.payload
    })),
    on(QuoteActions.getReal, (state, action) => ({
        ...state,
        real: action.payload
    }))
)

export function QuoteReducer(state: QuoteState, action: Action) {
    return reducer(state, action);
}
