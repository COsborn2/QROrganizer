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


