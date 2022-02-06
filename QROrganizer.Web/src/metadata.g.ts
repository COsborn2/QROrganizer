import {
  Domain, getEnumMeta, solidify, ModelType, ObjectType,
  PrimitiveProperty, ForeignKeyProperty, PrimaryKeyProperty,
  ModelCollectionNavigationProperty, ModelReferenceNavigationProperty
} from 'coalesce-vue/lib/metadata'


const domain: Domain = { enums: {}, types: {}, services: {} }
export const ApplicationUser = domain.types.ApplicationUser = {
  name: "ApplicationUser",
  displayName: "Application User",
  get displayProp() { return this.props.id }, 
  type: "model",
  controllerRoute: "ApplicationUser",
  get keyProp() { return this.props.id }, 
  behaviorFlags: 2,
  props: {
    id: {
      name: "id",
      displayName: "Id",
      type: "string",
      role: "primaryKey",
      hidden: 3,
    },
    userName: {
      name: "userName",
      displayName: "User Name",
      type: "string",
      role: "value",
    },
    normalizedUserName: {
      name: "normalizedUserName",
      displayName: "Normalized User Name",
      type: "string",
      role: "value",
    },
    email: {
      name: "email",
      displayName: "Email",
      type: "string",
      role: "value",
    },
    normalizedEmail: {
      name: "normalizedEmail",
      displayName: "Normalized Email",
      type: "string",
      role: "value",
    },
    emailConfirmed: {
      name: "emailConfirmed",
      displayName: "Email Confirmed",
      type: "boolean",
      role: "value",
    },
    passwordHash: {
      name: "passwordHash",
      displayName: "Password Hash",
      type: "string",
      role: "value",
    },
    securityStamp: {
      name: "securityStamp",
      displayName: "Security Stamp",
      type: "string",
      role: "value",
    },
    concurrencyStamp: {
      name: "concurrencyStamp",
      displayName: "Concurrency Stamp",
      type: "string",
      role: "value",
    },
    phoneNumber: {
      name: "phoneNumber",
      displayName: "Phone Number",
      type: "string",
      role: "value",
    },
    phoneNumberConfirmed: {
      name: "phoneNumberConfirmed",
      displayName: "Phone Number Confirmed",
      type: "boolean",
      role: "value",
    },
    twoFactorEnabled: {
      name: "twoFactorEnabled",
      displayName: "Two Factor Enabled",
      type: "boolean",
      role: "value",
    },
    lockoutEnd: {
      name: "lockoutEnd",
      displayName: "Lockout End",
      dateKind: "datetime",
      type: "date",
      role: "value",
    },
    lockoutEnabled: {
      name: "lockoutEnabled",
      displayName: "Lockout Enabled",
      type: "boolean",
      role: "value",
    },
    accessFailedCount: {
      name: "accessFailedCount",
      displayName: "Access Failed Count",
      type: "number",
      role: "value",
    },
  },
  methods: {
  },
  dataSources: {
  },
}
export const RestrictedAccessCode = domain.types.RestrictedAccessCode = {
  name: "RestrictedAccessCode",
  displayName: "Restricted Access Code",
  get displayProp() { return this.props.id }, 
  type: "model",
  controllerRoute: "RestrictedAccessCode",
  get keyProp() { return this.props.id }, 
  behaviorFlags: 6,
  props: {
    id: {
      name: "id",
      displayName: "Id",
      type: "number",
      role: "primaryKey",
      hidden: 3,
    },
    accessCode: {
      name: "accessCode",
      displayName: "Access Code",
      type: "string",
      role: "value",
    },
    numberOfUsesRemaining: {
      name: "numberOfUsesRemaining",
      displayName: "Number Of Uses Remaining",
      type: "number",
      role: "value",
    },
    isLimitedKey: {
      name: "isLimitedKey",
      displayName: "Is Limited Key",
      type: "boolean",
      role: "value",
    },
  },
  methods: {
    createUnlimitedUseAccessCode: {
      name: "createUnlimitedUseAccessCode",
      displayName: "Create Unlimited Use Access Code",
      transportType: "item",
      httpMethod: "POST",
      isStatic: true,
      params: {
      },
      return: {
        name: "$return",
        displayName: "Result",
        type: "void",
        role: "value",
      },
    },
    createAccessCode: {
      name: "createAccessCode",
      displayName: "Create Access Code",
      transportType: "item",
      httpMethod: "POST",
      isStatic: true,
      params: {
        numberOfUses: {
          name: "numberOfUses",
          displayName: "Number Of Uses",
          type: "number",
          role: "value",
        },
      },
      return: {
        name: "$return",
        displayName: "Result",
        type: "void",
        role: "value",
      },
    },
  },
  dataSources: {
  },
}
export const SiteInfoDto = domain.types.SiteInfoDto = {
  name: "SiteInfoDto",
  displayName: "Site Info Dto",
  type: "object",
  props: {
    buildDate: {
      name: "buildDate",
      displayName: "Build Date",
      dateKind: "datetime",
      type: "date",
      role: "value",
    },
    restrictedEnvironment: {
      name: "restrictedEnvironment",
      displayName: "Restricted Environment",
      type: "boolean",
      role: "value",
    },
  },
}
export const UserInfo = domain.types.UserInfo = {
  name: "UserInfo",
  displayName: "User Info",
  type: "object",
  props: {
    email: {
      name: "email",
      displayName: "Email",
      type: "string",
      role: "value",
    },
    username: {
      name: "username",
      displayName: "Username",
      type: "string",
      role: "value",
    },
    roles: {
      name: "roles",
      displayName: "Roles",
      type: "collection",
      itemType: {
        name: "$collectionItem",
        displayName: "",
        role: "value",
        type: "string",
      },
      role: "value",
    },
  },
}
export const AccessCodeService = domain.services.AccessCodeService = {
  name: "AccessCodeService",
  displayName: "Access Code Service",
  type: "service",
  controllerRoute: "AccessCodeService",
  methods: {
    isAccessCodeValid: {
      name: "isAccessCodeValid",
      displayName: "Is Access Code Valid",
      transportType: "item",
      httpMethod: "POST",
      params: {
        code: {
          name: "code",
          displayName: "Code",
          type: "string",
          role: "value",
        },
      },
      return: {
        name: "$return",
        displayName: "Result",
        type: "boolean",
        role: "value",
      },
    },
  },
}
export const SiteInfoService = domain.services.SiteInfoService = {
  name: "SiteInfoService",
  displayName: "Site Info Service",
  type: "service",
  controllerRoute: "SiteInfoService",
  methods: {
    getSiteInfo: {
      name: "getSiteInfo",
      displayName: "Get Site Info",
      transportType: "item",
      httpMethod: "POST",
      params: {
      },
      return: {
        name: "$return",
        displayName: "Result",
        type: "object",
        get typeDef() { return (domain.types.SiteInfoDto as ObjectType) },
        role: "value",
      },
    },
  },
}
export const UserInfoService = domain.services.UserInfoService = {
  name: "UserInfoService",
  displayName: "User Info Service",
  type: "service",
  controllerRoute: "UserInfoService",
  methods: {
    getUserInfo: {
      name: "getUserInfo",
      displayName: "Get User Info",
      transportType: "item",
      httpMethod: "POST",
      params: {
      },
      return: {
        name: "$return",
        displayName: "Result",
        type: "object",
        get typeDef() { return (domain.types.UserInfo as ObjectType) },
        role: "value",
      },
    },
  },
}

interface AppDomain extends Domain {
  enums: {
  }
  types: {
    ApplicationUser: typeof ApplicationUser
    RestrictedAccessCode: typeof RestrictedAccessCode
    SiteInfoDto: typeof SiteInfoDto
    UserInfo: typeof UserInfo
  }
  services: {
    AccessCodeService: typeof AccessCodeService
    SiteInfoService: typeof SiteInfoService
    UserInfoService: typeof UserInfoService
  }
}

solidify(domain)

export default domain as AppDomain
