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
  behaviorFlags: 7,
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

interface AppDomain extends Domain {
  enums: {
  }
  types: {
    ApplicationUser: typeof ApplicationUser
  }
  services: {
  }
}

solidify(domain)

export default domain as AppDomain
