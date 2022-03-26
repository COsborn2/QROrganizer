import * as $metadata from './metadata.g'
import * as $models from './models.g'
import * as qs from 'qs'
import { AxiosClient, ModelApiClient, ServiceApiClient, ItemResult, ListResult } from 'coalesce-vue/lib/api-client'
import { AxiosPromise, AxiosResponse, AxiosRequestConfig } from 'axios'

export class ApplicationUserApiClient extends ModelApiClient<$models.ApplicationUser> {
  constructor() { super($metadata.ApplicationUser) }
}


export class ContainerApiClient extends ModelApiClient<$models.Container> {
  constructor() { super($metadata.Container) }
}


export class ItemApiClient extends ModelApiClient<$models.Item> {
  constructor() { super($metadata.Item) }
}


export class ItemBarcodeInformationApiClient extends ModelApiClient<$models.ItemBarcodeInformation> {
  constructor() { super($metadata.ItemBarcodeInformation) }
}


export class LogApiClient extends ModelApiClient<$models.Log> {
  constructor() { super($metadata.Log) }
}


export class RestrictedAccessCodeApiClient extends ModelApiClient<$models.RestrictedAccessCode> {
  constructor() { super($metadata.RestrictedAccessCode) }
  public createUnlimitedUseAccessCode($config?: AxiosRequestConfig): AxiosPromise<ItemResult<void>> {
    const $method = this.$metadata.methods.createUnlimitedUseAccessCode
    const $params =  {
    }
    return this.$invoke($method, $params, $config)
  }
  
  public createAccessCode(numberOfUses: number | null, $config?: AxiosRequestConfig): AxiosPromise<ItemResult<void>> {
    const $method = this.$metadata.methods.createAccessCode
    const $params =  {
      numberOfUses,
    }
    return this.$invoke($method, $params, $config)
  }
  
}


export class SubscriptionFeatureApiClient extends ModelApiClient<$models.SubscriptionFeature> {
  constructor() { super($metadata.SubscriptionFeature) }
}


export class SubscriptionLevelApiClient extends ModelApiClient<$models.SubscriptionLevel> {
  constructor() { super($metadata.SubscriptionLevel) }
}


export class AccessCodeServiceApiClient extends ServiceApiClient<typeof $metadata.AccessCodeService> {
  constructor() { super($metadata.AccessCodeService) }
  public isAccessCodeValid(code: string | null, $config?: AxiosRequestConfig): AxiosPromise<ItemResult<boolean>> {
    const $method = this.$metadata.methods.isAccessCodeValid
    const $params =  {
      code,
    }
    return this.$invoke($method, $params, $config)
  }
  
}


export class ItemScanningServiceApiClient extends ServiceApiClient<typeof $metadata.ItemScanningService> {
  constructor() { super($metadata.ItemScanningService) }
  public createItemForUpcCodeAndStartSearch(upcCode: string | null, containerId: number | null, $config?: AxiosRequestConfig): AxiosPromise<ItemResult<$models.Item>> {
    const $method = this.$metadata.methods.createItemForUpcCodeAndStartSearch
    const $params =  {
      upcCode,
      containerId,
    }
    return this.$invoke($method, $params, $config)
  }
  
}


export class SiteInfoServiceApiClient extends ServiceApiClient<typeof $metadata.SiteInfoService> {
  constructor() { super($metadata.SiteInfoService) }
  public getSiteInfo($config?: AxiosRequestConfig): AxiosPromise<ItemResult<$models.SiteInfoDto>> {
    const $method = this.$metadata.methods.getSiteInfo
    const $params =  {
    }
    return this.$invoke($method, $params, $config)
  }
  
}


export class UserInfoServiceApiClient extends ServiceApiClient<typeof $metadata.UserInfoService> {
  constructor() { super($metadata.UserInfoService) }
  public getUserInfo($config?: AxiosRequestConfig): AxiosPromise<ItemResult<$models.UserInfo>> {
    const $method = this.$metadata.methods.getUserInfo
    const $params =  {
    }
    return this.$invoke($method, $params, $config)
  }
  
  public confirmEmail(userId: string | null, confirmationToken: string | null, $config?: AxiosRequestConfig): AxiosPromise<ItemResult<void>> {
    const $method = this.$metadata.methods.confirmEmail
    const $params =  {
      userId,
      confirmationToken,
    }
    return this.$invoke($method, $params, $config)
  }
  
}


