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


export interface ContainerViewModel extends $models.Container {
  id: number | null;
  containerName: string | null;
  userId: string | null;
  items: ItemViewModel[] | null;
}
export class ContainerViewModel extends ViewModel<$models.Container, $apiClients.ContainerApiClient, number> implements $models.Container  {
  
  
  public addToItems() {
    return this.$addChild('items') as ItemViewModel
  }
  
  constructor(initialData?: DeepPartial<$models.Container> | null) {
    super($metadata.Container, new $apiClients.ContainerApiClient(), initialData)
  }
}
defineProps(ContainerViewModel, $metadata.Container)

export class ContainerListViewModel extends ListViewModel<$models.Container, $apiClients.ContainerApiClient, ContainerViewModel> {
  
  constructor() {
    super($metadata.Container, new $apiClients.ContainerApiClient())
  }
}


export interface ItemViewModel extends $models.Item {
  id: number | null;
  barcodeNumber: string | null;
  name: string | null;
  quantity: number | null;
  userId: string | null;
  user: ApplicationUserViewModel | null;
  containerId: number | null;
  container: ContainerViewModel | null;
}
export class ItemViewModel extends ViewModel<$models.Item, $apiClients.ItemApiClient, number> implements $models.Item  {
  
  constructor(initialData?: DeepPartial<$models.Item> | null) {
    super($metadata.Item, new $apiClients.ItemApiClient(), initialData)
  }
}
defineProps(ItemViewModel, $metadata.Item)

export class ItemListViewModel extends ListViewModel<$models.Item, $apiClients.ItemApiClient, ItemViewModel> {
  
  constructor() {
    super($metadata.Item, new $apiClients.ItemApiClient())
  }
}


export interface LogViewModel extends $models.Log {
  id: number | null;
  application: string | null;
  date: string | null;
  level: string | null;
  message: string | null;
  callSite: string | null;
  exception: string | null;
  user: string | null;
  url: string | null;
  urlReferrer: string | null;
  browser: string | null;
}
export class LogViewModel extends ViewModel<$models.Log, $apiClients.LogApiClient, number> implements $models.Log  {
  
  constructor(initialData?: DeepPartial<$models.Log> | null) {
    super($metadata.Log, new $apiClients.LogApiClient(), initialData)
  }
}
defineProps(LogViewModel, $metadata.Log)

export class LogListViewModel extends ListViewModel<$models.Log, $apiClients.LogApiClient, LogViewModel> {
  
  constructor() {
    super($metadata.Log, new $apiClients.LogApiClient())
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
  
  public get confirmEmail() {
    const confirmEmail = this.$apiClient.$makeCaller(
      this.$metadata.methods.confirmEmail,
      (c, userId: string | null, confirmationToken: string | null) => c.confirmEmail(userId, confirmationToken),
      () => ({userId: null as string | null, confirmationToken: null as string | null, }),
      (c, args) => c.confirmEmail(args.userId, args.confirmationToken))
    
    Object.defineProperty(this, 'confirmEmail', {value: confirmEmail});
    return confirmEmail
  }
  
  constructor() {
    super($metadata.UserInfoService, new $apiClients.UserInfoServiceApiClient())
  }
}


const viewModelTypeLookup = ViewModel.typeLookup = {
  ApplicationUser: ApplicationUserViewModel,
  Container: ContainerViewModel,
  Item: ItemViewModel,
  Log: LogViewModel,
  RestrictedAccessCode: RestrictedAccessCodeViewModel,
}
const listViewModelTypeLookup = ListViewModel.typeLookup = {
  ApplicationUser: ApplicationUserListViewModel,
  Container: ContainerListViewModel,
  Item: ItemListViewModel,
  Log: LogListViewModel,
  RestrictedAccessCode: RestrictedAccessCodeListViewModel,
}
const serviceViewModelTypeLookup = ServiceViewModel.typeLookup = {
  AccessCodeService: AccessCodeServiceViewModel,
  SiteInfoService: SiteInfoServiceViewModel,
  UserInfoService: UserInfoServiceViewModel,
}

