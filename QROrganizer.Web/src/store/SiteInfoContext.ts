import {SiteInfoDto} from "@/models.g";
import {Module} from "vuex";
import {SiteInfoServiceViewModel} from "@/viewmodels.g";

export enum SiteInfoMutations {
  SET_SITE_INFO = "SET_SITE_INFO"
}

export enum SiteInfoActions {
  GET_AND_SET_SITE_INFO = "GET_AND_SET_SITE_INFO"
}

const siteInfoService = new SiteInfoServiceViewModel();
export const siteInfo: Module<SiteInfoDto, any> = {
  state: () => new SiteInfoDto(),
  mutations: {
    [SiteInfoMutations.SET_SITE_INFO](state, payload: SiteInfoDto) {
      if (payload?.restrictedEnvironment !== null) {
        state.restrictedEnvironment = payload.restrictedEnvironment;
      }

      if (payload?.buildDate !== null) {
        state.buildDate = payload.buildDate;
      }
    }
  },
  getters: {
    isRestrictedEnvironment: (state) => {
      return state?.restrictedEnvironment === true;
    }
  },
  actions: {
    async [SiteInfoActions.GET_AND_SET_SITE_INFO]({commit}): Promise<void> {
      await siteInfoService.getSiteInfo();
      let result = siteInfoService.getSiteInfo.result;

      commit(SiteInfoMutations.SET_SITE_INFO, result);
    }
  }
}
