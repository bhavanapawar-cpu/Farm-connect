import { createAction, props } from '@ngrx/store';
import { AuthResponse } from '../services/auth.service';

export const login = createAction(
  '[Auth] Login',
  props<{ email: string; password: string }>()
);

export const loginSuccess = createAction(
  '[Auth] Login Success',
  props<{ user: AuthResponse }>()
);

export const loginFailure = createAction(
  '[Auth] Login Failure',
  props<{ error: string }>()
);

export const register = createAction(
  '[Auth] Register',
  props<{ firstName: string; lastName: string; email: string; password: string; phoneNumber: string; role: string }>()
);

export const registerSuccess = createAction(
  '[Auth] Register Success',
  props<{ user: AuthResponse }>()
);

export const registerFailure = createAction(
  '[Auth] Register Failure',
  props<{ error: string }>()
);

export const logout = createAction(
  '[Auth] Logout'
);

export const loadUser = createAction(
  '[Auth] Load User'
);

export const loadUserSuccess = createAction(
  '[Auth] Load User Success',
  props<{ user: AuthResponse }>()
);

export const loadUserFailure = createAction(
  '[Auth] Load User Failure',
  props<{ error: string }>()
);
