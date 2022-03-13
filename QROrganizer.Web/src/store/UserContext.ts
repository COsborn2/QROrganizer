import { Module } from 'vuex';
import {UserInfo} from "@/models.g";
import {AxiosClient} from "coalesce-vue/lib/api-client";
import Vue from "vue";
import {ApplicationInsights} from "@microsoft/applicationinsights-web";

export enum UserMutations {
  SET_ACCOUNT = 'SET_ACCOUNT',
  REMOVE_ACCOUNT = 'REMOVE_ACCOUNT'
}

export enum UserActions {
  LOGOUT = "LOGOUT"
}

export const user: Module<UserInfo, any> = {
  state: () => new UserInfo(),
  mutations: {
    [UserMutations.SET_ACCOUNT](state, payload: UserInfo) {
      if (payload) {
        if ((payload.email?.length ?? 0) > 0) {
          state.email = payload.email;
        }
        if ((payload.roles?.length ?? 0) > 0) {
          state.roles = payload.roles?.map(x => x.toLowerCase()) ?? [];
        }
        if ((payload.username?.length ?? 0) > 0) {
          state.username = payload.username;
          if (payload.username) {
            (Vue.prototype.$appInsights as ApplicationInsights).setAuthenticatedUserContext(
              payload.username,
              undefined,
              true);
          }
        }
      }
    },
    [UserMutations.REMOVE_ACCOUNT](state) {
      state.email = null;
      state.roles = [];
    }
  },
  getters: {
    isAuthenticated: (state) => {
      return (state?.email?.length ?? 0) > 0;
    },
    roles: (state) => {
      return state?.roles ?? [];
    }
  },
  actions: {
    async [UserActions.LOGOUT]({commit}): Promise<void> {
      await AxiosClient.post('/logout');
      commit(UserMutations.REMOVE_ACCOUNT);
    }
  }
};
