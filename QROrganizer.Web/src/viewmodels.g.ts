import * as $metadata from './metadata.g'
import * as $models from './models.g'
import * as $apiClients from './api-clients.g'
import { ViewModel, ListViewModel, ServiceViewModel, DeepPartial, defineProps } from 'coalesce-vue/lib/viewmodel'

export interface ApplicationUserViewModel extends $models.ApplicationUser {
  id: string | null;
  userName: string | null;
  normalizedUserName: string | null;
  email: string | null;
  normalizedEmail: string | null;
  emailConfirmed: boolean | null;
  passwordHash: string | null;
  securityStamp: string | null;
  concurrencyStamp: string | null;
  phoneNumber: string | null;
  phoneNumberConfirmed: boolean | null;
  twoFactorEnabled: boolean | null;
  lockoutEnd: Date | null;
  lockoutEnabled: boolean | null;
  accessFailedCount: number | null;
}
export class ApplicationUserViewModel extends ViewModel<$models.ApplicationUser, $apiClients.ApplicationUserApiClient, string> implements $models.ApplicationUser  {
  
  constructor(initialData?: DeepPartial<$models.ApplicationUser> | null) {
    super($metadata.ApplicationUser, new $apiClients.ApplicationUserApiClient(), initialData)
  }
}
defineProps(ApplicationUserViewModel, $metadata.ApplicationUser)

export class ApplicationUserListViewModel extends ListViewModel<$models.ApplicationUser, $apiClients.ApplicationUserApiClient, ApplicationUserViewModel> {
  
  constructor() {
    super($metadata.ApplicationUser, new $apiClients.ApplicationUserApiClient())
  }
}


export interface RestrictedAccessCodeViewModel extends $models.RestrictedAccessCode {
  id: number | null;
  accessCode: string | null;
  numberOfUsesRemaining: number | null;
  isLimitedKey: boolean | null;
}
export class RestrictedAccessCodeViewModel extends ViewModel<$models.RestrictedAccessCode, $apiClients.RestrictedAccessCodeApiClient, number> implements $models.RestrictedAccessCode  {
  
  constructor(initialData?: DeepPartial<$models.RestrictedAccessCode> | null) {
    super($metadata.RestrictedAccessCode, new $apiClients.RestrictedAccessCodeApiClient(), initialData)
  }
}
defineProps(RestrictedAccessCodeViewModel, $metadata.RestrictedAccessCode)

export class RestrictedAccessCodeListViewModel extends ListViewModel<$models.RestrictedAccessCode, $apiClients.RestrictedAccessCodeApiClient, RestrictedAccessCodeViewModel> {
  
  public get createUnlimitedUseAccessCode() {
    const createUnlimitedUseAccessCode = this.$apiClient.$makeCaller(
      this.$metadata.methods.createUnlimitedUseAccessCode,
      (c) => c.createUnlimitedUseAccessCode(),
      () => ({}),
      (c, args) => c.createUnlimitedUseAccessCode())
    
    Object.defineProperty(this, 'createUnlimitedUseAccessCode', {value: createUnlimitedUseAccessCode});
    return createUnlimitedUseAccessCode
  }
  
  public get createAccessCode() {
    const createAccessCode = this.$apiClient.$makeCaller(
      this.$metadata.methods.createAccessCode,
      (c, numberOfUses: number | null) => c.createAccessCode(numberOfUses),
      () => ({numberOfUses: null as number | null, }),
      (c, args) => c.createAccessCode(args.numberOfUses))
    
    Object.defineProperty(this, 'createAccessCode', {value: createAccessCode});
    return createAccessCode
  }
  
  constructor() {
    super($metadata.RestrictedAccessCode, new $apiClients.RestrictedAccessCodeApiClient())
  }
}


export class AccessCodeServiceViewModel extends ServiceViewModel<typeof $metadata.AccessCodeService, $apiClients.AccessCodeServiceApiClient> {
  
  public get isAccessCodeValid() {
    const isAccessCodeValid = this.$apiClient.$makeCaller(
      this.$metadata.methods.isAccessCodeValid,
      (c, code: string | null) => c.isAccessCodeValid(code),
      () => ({code: null as string | null, }),
      (c, args) => c.isAccessCodeValid(args.code))
    
    Object.defineProperty(this, 'isAccessCodeValid', {value: isAccessCodeValid});
    return isAccessCodeValid
  }
  
  constructor() {
    super($metadata.AccessCodeService, new $apiClients.AccessCodeServiceApiClient())
  }
}


export class SiteInfoServiceViewModel extends ServiceViewModel<typeof $metadata.SiteInfoService, $apiClients.SiteInfoServiceApiClient> {
  
  public get getSiteInfo() {
    const getSiteInfo = this.$apiClient.$makeCaller(
      this.$metadata.methods.getSiteInfo,
      (c) => c.getSiteInfo(),
      () => ({}),
      (c, args) => c.getSiteInfo())
    
    Object.defineProperty(this, 'getSiteInfo', {value: getSiteInfo});
    return getSiteInfo
  }
  
  constructor() {
    super($metadata.SiteInfoService, new $apiClients.SiteInfoServiceApiClient())
  }
}


export class UserInfoServiceViewModel extends ServiceViewModel<typeof $metadata.UserInfoService, $apiClients.UserInfoServiceApiClient> {
  
  public get getUserInfo() {
    const getUserInfo = this.$apiClient.$makeCaller(
      this.$metadata.methods.getUserInfo,
      (c) => c.getUserInfo(),
      () => ({}),
      (c, args) => c.getUserInfo())
    
    Object.defineProperty(this, 'getUserInfo', {value: getUserInfo});
    return getUserInfo
  }
  
  constructor() {
    super($metadata.UserInfoService, new $apiClients.UserInfoServiceApiClient())
  }
}


const viewModelTypeLookup = ViewModel.typeLookup = {
  ApplicationUser: ApplicationUserViewModel,
  RestrictedAccessCode: RestrictedAccessCodeViewModel,
}
const listViewModelTypeLookup = ListViewModel.typeLookup = {
  ApplicationUser: ApplicationUserListViewModel,
  RestrictedAccessCode: RestrictedAccessCodeListViewModel,
}
const serviceViewModelTypeLookup = ServiceViewModel.typeLookup = {
  AccessCodeService: AccessCodeServiceViewModel,
  SiteInfoService: SiteInfoServiceViewModel,
  UserInfoService: UserInfoServiceViewModel,
}

