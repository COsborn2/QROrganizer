import * as metadata from './metadata.g'
import { Model, DataSource, convertToModel, mapToModel } from 'coalesce-vue/lib/model'

export interface ApplicationUser extends Model<typeof metadata.ApplicationUser> {
  id: string | null
  userName: string | null
  normalizedUserName: string | null
  email: string | null
  normalizedEmail: string | null
  emailConfirmed: boolean | null
  passwordHash: string | null
  securityStamp: string | null
  concurrencyStamp: string | null
  phoneNumber: string | null
  phoneNumberConfirmed: boolean | null
  twoFactorEnabled: boolean | null
  lockoutEnd: Date | null
  lockoutEnabled: boolean | null
  accessFailedCount: number | null
}
export class ApplicationUser {
  
  /** Mutates the input object and its descendents into a valid ApplicationUser implementation. */
  static convert(data?: Partial<ApplicationUser>): ApplicationUser {
    return convertToModel(data || {}, metadata.ApplicationUser) 
  }
  
  /** Maps the input object and its descendents to a new, valid ApplicationUser implementation. */
  static map(data?: Partial<ApplicationUser>): ApplicationUser {
    return mapToModel(data || {}, metadata.ApplicationUser) 
  }
  
  /** Instantiate a new ApplicationUser, optionally basing it on the given data. */
  constructor(data?: Partial<ApplicationUser> | {[k: string]: any}) {
      Object.assign(this, ApplicationUser.map(data || {}));
  }
}


export interface Container extends Model<typeof metadata.Container> {
  id: number | null
  containerName: string | null
  userId: string | null
  items: Item[] | null
}
export class Container {
  
  /** Mutates the input object and its descendents into a valid Container implementation. */
  static convert(data?: Partial<Container>): Container {
    return convertToModel(data || {}, metadata.Container) 
  }
  
  /** Maps the input object and its descendents to a new, valid Container implementation. */
  static map(data?: Partial<Container>): Container {
    return mapToModel(data || {}, metadata.Container) 
  }
  
  /** Instantiate a new Container, optionally basing it on the given data. */
  constructor(data?: Partial<Container> | {[k: string]: any}) {
      Object.assign(this, Container.map(data || {}));
  }
}
export namespace Container {
  export namespace DataSources {
    
    export class DefaultDataSource implements DataSource<typeof metadata.Container.dataSources.defaultDataSource> {
      readonly $metadata = metadata.Container.dataSources.defaultDataSource
    }
  }
}


export interface Item extends Model<typeof metadata.Item> {
  id: number | null
  barcodeNumber: string | null
  name: string | null
  quantity: number | null
  userId: string | null
  user: ApplicationUser | null
  containerId: number | null
  container: Container | null
}
export class Item {
  
  /** Mutates the input object and its descendents into a valid Item implementation. */
  static convert(data?: Partial<Item>): Item {
    return convertToModel(data || {}, metadata.Item) 
  }
  
  /** Maps the input object and its descendents to a new, valid Item implementation. */
  static map(data?: Partial<Item>): Item {
    return mapToModel(data || {}, metadata.Item) 
  }
  
  /** Instantiate a new Item, optionally basing it on the given data. */
  constructor(data?: Partial<Item> | {[k: string]: any}) {
      Object.assign(this, Item.map(data || {}));
  }
}
export namespace Item {
  export namespace DataSources {
    
    export class DefaultDataSource implements DataSource<typeof metadata.Item.dataSources.defaultDataSource> {
      readonly $metadata = metadata.Item.dataSources.defaultDataSource
    }
    
    export class ItemsInContainer implements DataSource<typeof metadata.Item.dataSources.itemsInContainer> {
      readonly $metadata = metadata.Item.dataSources.itemsInContainer
      containerId: number | null = null
    }
  }
}


export interface Log extends Model<typeof metadata.Log> {
  id: number | null
  application: string | null
  date: string | null
  level: string | null
  message: string | null
  callSite: string | null
  exception: string | null
  user: string | null
  url: string | null
  urlReferrer: string | null
  browser: string | null
}
export class Log {
  
  /** Mutates the input object and its descendents into a valid Log implementation. */
  static convert(data?: Partial<Log>): Log {
    return convertToModel(data || {}, metadata.Log) 
  }
  
  /** Maps the input object and its descendents to a new, valid Log implementation. */
  static map(data?: Partial<Log>): Log {
    return mapToModel(data || {}, metadata.Log) 
  }
  
  /** Instantiate a new Log, optionally basing it on the given data. */
  constructor(data?: Partial<Log> | {[k: string]: any}) {
      Object.assign(this, Log.map(data || {}));
  }
}


export interface RestrictedAccessCode extends Model<typeof metadata.RestrictedAccessCode> {
  id: number | null
  accessCode: string | null
  numberOfUsesRemaining: number | null
  isLimitedKey: boolean | null
}
export class RestrictedAccessCode {
  
  /** Mutates the input object and its descendents into a valid RestrictedAccessCode implementation. */
  static convert(data?: Partial<RestrictedAccessCode>): RestrictedAccessCode {
    return convertToModel(data || {}, metadata.RestrictedAccessCode) 
  }
  
  /** Maps the input object and its descendents to a new, valid RestrictedAccessCode implementation. */
  static map(data?: Partial<RestrictedAccessCode>): RestrictedAccessCode {
    return mapToModel(data || {}, metadata.RestrictedAccessCode) 
  }
  
  /** Instantiate a new RestrictedAccessCode, optionally basing it on the given data. */
  constructor(data?: Partial<RestrictedAccessCode> | {[k: string]: any}) {
      Object.assign(this, RestrictedAccessCode.map(data || {}));
  }
}


export interface SiteInfoDto extends Model<typeof metadata.SiteInfoDto> {
  buildDate: Date | null
  restrictedEnvironment: boolean | null
}
export class SiteInfoDto {
  
  /** Mutates the input object and its descendents into a valid SiteInfoDto implementation. */
  static convert(data?: Partial<SiteInfoDto>): SiteInfoDto {
    return convertToModel(data || {}, metadata.SiteInfoDto) 
  }
  
  /** Maps the input object and its descendents to a new, valid SiteInfoDto implementation. */
  static map(data?: Partial<SiteInfoDto>): SiteInfoDto {
    return mapToModel(data || {}, metadata.SiteInfoDto) 
  }
  
  /** Instantiate a new SiteInfoDto, optionally basing it on the given data. */
  constructor(data?: Partial<SiteInfoDto> | {[k: string]: any}) {
      Object.assign(this, SiteInfoDto.map(data || {}));
  }
}


export interface UserInfo extends Model<typeof metadata.UserInfo> {
  email: string | null
  username: string | null
  roles: string[] | null
}
export class UserInfo {
  
  /** Mutates the input object and its descendents into a valid UserInfo implementation. */
  static convert(data?: Partial<UserInfo>): UserInfo {
    return convertToModel(data || {}, metadata.UserInfo) 
  }
  
  /** Maps the input object and its descendents to a new, valid UserInfo implementation. */
  static map(data?: Partial<UserInfo>): UserInfo {
    return mapToModel(data || {}, metadata.UserInfo) 
  }
  
  /** Instantiate a new UserInfo, optionally basing it on the given data. */
  constructor(data?: Partial<UserInfo> | {[k: string]: any}) {
      Object.assign(this, UserInfo.map(data || {}));
  }
}


