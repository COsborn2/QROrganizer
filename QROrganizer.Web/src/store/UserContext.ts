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

function setString(from: any, to: any, prop: string) {
  if ((from[prop]?.length ?? 0) > 0) {
    to[prop] = from[prop];
  }
}

function setArrayLower(from: any, to: any, prop: string) {
  if ((from[prop]?.length ?? 0) > 0) {
    to[prop] = from[prop]?.map((x: any) => x.toLowerCase()) ?? [];
  }
}

export const user: Module<UserInfo, any> = {
  state: () => new UserInfo(),
  mutations: {
    [UserMutations.SET_ACCOUNT](state, payload: UserInfo) {
      if (payload) {
        setString(payload, state, 'email');
        setString(payload, state, 'subscriptionName');

        setArrayLower(payload, state, 'roles');
        setArrayLower(payload, state, 'activeFeatures');
        setArrayLower(payload, state, 'inactiveFeatures');

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
      state.email = state.username = state.subscriptionName = null;
      state.roles = state.activeFeatures = state.inactiveFeatures = [];
    }
  },
  getters: {
    isAuthenticated: (state) => {
      return (state?.email?.length ?? 0) > 0;
    },
    roles: (state) => {
      return state?.roles ?? [];
    },
    activeFeatures: (state) => {
      return state?.activeFeatures ?? [];
    },
    inactiveFeatures: (state) => {
      return state?.inactiveFeatures ?? [];
    }
  },
  actions: {
    async [UserActions.LOGOUT]({commit}): Promise<void> {
      await AxiosClient.post('/logout');
      commit(UserMutations.REMOVE_ACCOUNT);
    }
  }
};
