import { Action, createAction, props, createReducer, on } from '@ngrx/store';  
import { quote } from '../../models/quote.model';

enum ActionTypes {
  GetDolar = 'GetDolar',
  GetReal = 'GetReal'
}

export const getDolar = createAction(
  ActionTypes.GetDolar,
  props<{payload: quote}>()
)

export const getReal = createAction(
  ActionTypes.GetReal,
  props<{payload: quote}>()
)