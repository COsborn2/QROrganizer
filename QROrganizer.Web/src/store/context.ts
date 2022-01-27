import { Module } from 'vuex';
import { UserState } from '@/store/UserState';
import axios from 'axios';

const state: UserState = {
  username: '',
  email: '',
};

export enum UserMutations {
  SET_ACCOUNT = 'SET_ACCOUNT',
}

export const user: Module<any, UserState> = {
  state,
  mutations: {
    [UserMutations.SET_ACCOUNT](state, payload: UserState) {
      state.username = payload.username;
      state.email = payload.email;
    },
  },
  getters: {
    isAuthenticated: (state) => state?.username !== null,
  },
  actions: {
    login({ commit }, credentials) {
      return axios.post<UserState>('account/login', credentials).then((res) => {
        commit(UserMutations.SET_ACCOUNT, res);
      });
    },
  },
};
