import * as $metadata from './metadata.g'
import * as $models from './models.g'
import * as qs from 'qs'
import { AxiosClient, ModelApiClient, ServiceApiClient, ItemResult, ListResult } from 'coalesce-vue/lib/api-client'
import { AxiosPromise, AxiosResponse, AxiosRequestConfig } from 'axios'

export class ApplicationUserApiClient extends ModelApiClient<$models.ApplicationUser> {
  constructor() { super($metadata.ApplicationUser) }
}


export class UserServiceApiClient extends ServiceApiClient<typeof $metadata.UserService> {
  constructor() { super($metadata.UserService) }
  public getUserInfo($config?: AxiosRequestConfig): AxiosPromise<ItemResult<$models.UserInfo>> {
    const $method = this.$metadata.methods.getUserInfo
    const $params =  {
    }
    return this.$invoke($method, $params, $config)
  }
  
}


