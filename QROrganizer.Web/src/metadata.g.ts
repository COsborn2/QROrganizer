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
    subscriptionLevelId: {
      name: "subscriptionLevelId",
      displayName: "Subscription Level Id",
      type: "number",
      role: "foreignKey",
      get principalKey() { return (domain.types.SubscriptionLevel as ModelType).props.id as PrimaryKeyProperty },
      get principalType() { return (domain.types.SubscriptionLevel as ModelType) },
      get navigationProp() { return (domain.types.ApplicationUser as ModelType).props.subscriptionLevel as ModelReferenceNavigationProperty },
      hidden: 3,
    },
    subscriptionLevel: {
      name: "subscriptionLevel",
      displayName: "Subscription Level",
      type: "model",
      get typeDef() { return (domain.types.SubscriptionLevel as ModelType) },
      role: "referenceNavigation",
      get foreignKey() { return (domain.types.ApplicationUser as ModelType).props.subscriptionLevelId as ForeignKeyProperty },
      get principalKey() { return (domain.types.SubscriptionLevel as ModelType).props.id as PrimaryKeyProperty },
      dontSerialize: true,
    },
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
export const Container = domain.types.Container = {
  name: "Container",
  displayName: "Container",
  get displayProp() { return this.props.id }, 
  type: "model",
  controllerRoute: "Container",
  get keyProp() { return this.props.id }, 
  behaviorFlags: 7,
  props: {
    id: {
      name: "id",
      displayName: "Id",
      type: "number",
      role: "primaryKey",
      hidden: 3,
    },
    containerName: {
      name: "containerName",
      displayName: "Container Name",
      type: "string",
      role: "value",
    },
    userId: {
      name: "userId",
      displayName: "User Id",
      type: "string",
      role: "foreignKey",
      get principalKey() { return (domain.types.ApplicationUser as ModelType).props.id as PrimaryKeyProperty },
      get principalType() { return (domain.types.ApplicationUser as ModelType) },
      get navigationProp() { return (domain.types.Container as ModelType).props.user as ModelReferenceNavigationProperty },
      hidden: 3,
      rules: {
        required: val => (val != null && val !== '') || "User is required.",
      }
    },
    items: {
      name: "items",
      displayName: "Items",
      type: "collection",
      itemType: {
        name: "$collectionItem",
        displayName: "",
        role: "value",
        type: "model",
        get typeDef() { return (domain.types.Item as ModelType) },
      },
      role: "collectionNavigation",
      get foreignKey() { return (domain.types.Item as ModelType).props.containerId as ForeignKeyProperty },
      get inverseNavigation() { return (domain.types.Item as ModelType).props.container as ModelReferenceNavigationProperty },
      dontSerialize: true,
    },
  },
  methods: {
  },
  dataSources: {
    defaultDataSource: {
      type: "dataSource",
      name: "DefaultDataSource",
      displayName: "Default Data Source",
      isDefault: true,
      props: {
      },
    },
  },
}
export const Item = domain.types.Item = {
  name: "Item",
  displayName: "Item",
  get displayProp() { return this.props.name }, 
  type: "model",
  controllerRoute: "Item",
  get keyProp() { return this.props.id }, 
  behaviorFlags: 7,
  props: {
    id: {
      name: "id",
      displayName: "Id",
      type: "number",
      role: "primaryKey",
      hidden: 3,
    },
    barcodeNumber: {
      name: "barcodeNumber",
      displayName: "Barcode Number",
      type: "string",
      role: "value",
    },
    name: {
      name: "name",
      displayName: "Name",
      type: "string",
      role: "value",
      rules: {
        required: val => (val != null && val !== '') || "Name is required.",
      }
    },
    quantity: {
      name: "quantity",
      displayName: "Quantity",
      type: "number",
      role: "value",
      rules: {
        required: val => val != null || "Quantity is required.",
      }
    },
    userId: {
      name: "userId",
      displayName: "User Id",
      type: "string",
      role: "foreignKey",
      get principalKey() { return (domain.types.ApplicationUser as ModelType).props.id as PrimaryKeyProperty },
      get principalType() { return (domain.types.ApplicationUser as ModelType) },
      get navigationProp() { return (domain.types.Item as ModelType).props.user as ModelReferenceNavigationProperty },
      hidden: 3,
      rules: {
        required: val => (val != null && val !== '') || "User is required.",
      }
    },
    user: {
      name: "user",
      displayName: "User",
      type: "model",
      get typeDef() { return (domain.types.ApplicationUser as ModelType) },
      role: "referenceNavigation",
      get foreignKey() { return (domain.types.Item as ModelType).props.userId as ForeignKeyProperty },
      get principalKey() { return (domain.types.ApplicationUser as ModelType).props.id as PrimaryKeyProperty },
      dontSerialize: true,
    },
    containerId: {
      name: "containerId",
      displayName: "Container Id",
      type: "number",
      role: "foreignKey",
      get principalKey() { return (domain.types.Container as ModelType).props.id as PrimaryKeyProperty },
      get principalType() { return (domain.types.Container as ModelType) },
      get navigationProp() { return (domain.types.Item as ModelType).props.container as ModelReferenceNavigationProperty },
      hidden: 3,
    },
    container: {
      name: "container",
      displayName: "Container",
      type: "model",
      get typeDef() { return (domain.types.Container as ModelType) },
      role: "referenceNavigation",
      get foreignKey() { return (domain.types.Item as ModelType).props.containerId as ForeignKeyProperty },
      get principalKey() { return (domain.types.Container as ModelType).props.id as PrimaryKeyProperty },
      get inverseNavigation() { return (domain.types.Container as ModelType).props.items as ModelCollectionNavigationProperty },
      dontSerialize: true,
    },
  },
  methods: {
  },
  dataSources: {
    defaultDataSource: {
      type: "dataSource",
      name: "DefaultDataSource",
      displayName: "Default Data Source",
      isDefault: true,
      props: {
      },
    },
    itemsInContainer: {
      type: "dataSource",
      name: "ItemsInContainer",
      displayName: "Items In Container",
      props: {
        containerId: {
          name: "containerId",
          displayName: "Container Id",
          type: "number",
          role: "value",
        },
      },
    },
  },
}
export const ItemBarcodeInformation = domain.types.ItemBarcodeInformation = {
  name: "ItemBarcodeInformation",
  displayName: "Item Barcode Information",
  get displayProp() { return this.props.id }, 
  type: "model",
  controllerRoute: "ItemBarcodeInformation",
  get keyProp() { return this.props.id }, 
  behaviorFlags: 7,
  props: {
    id: {
      name: "id",
      displayName: "Id",
      type: "number",
      role: "primaryKey",
      hidden: 3,
    },
    title: {
      name: "title",
      displayName: "Title",
      type: "string",
      role: "value",
    },
    upcCode: {
      name: "upcCode",
      displayName: "Upc Code",
      type: "string",
      role: "value",
    },
    eanCode: {
      name: "eanCode",
      displayName: "Ean Code",
      type: "string",
      role: "value",
    },
    parentCategory: {
      name: "parentCategory",
      displayName: "Parent Category",
      type: "string",
      role: "value",
    },
    category: {
      name: "category",
      displayName: "Category",
      type: "string",
      role: "value",
    },
    brand: {
      name: "brand",
      displayName: "Brand",
      type: "string",
      role: "value",
    },
    model: {
      name: "model",
      displayName: "Model",
      type: "string",
      role: "value",
    },
    mpnCode: {
      name: "mpnCode",
      displayName: "Mpn Code",
      type: "string",
      role: "value",
    },
    manufacturer: {
      name: "manufacturer",
      displayName: "Manufacturer",
      type: "string",
      role: "value",
    },
    publisher: {
      name: "publisher",
      displayName: "Publisher",
      type: "string",
      role: "value",
    },
    asinCode: {
      name: "asinCode",
      displayName: "Asin Code",
      type: "string",
      role: "value",
    },
    color: {
      name: "color",
      displayName: "Color",
      type: "string",
      role: "value",
    },
    size: {
      name: "size",
      displayName: "Size",
      type: "string",
      role: "value",
    },
    weight: {
      name: "weight",
      displayName: "Weight",
      type: "string",
      role: "value",
    },
    imageLink: {
      name: "imageLink",
      displayName: "Image Link",
      type: "string",
      role: "value",
    },
    isAdult: {
      name: "isAdult",
      displayName: "Is Adult",
      type: "string",
      role: "value",
    },
    description: {
      name: "description",
      displayName: "Description",
      type: "string",
      role: "value",
    },
    lowestPrice: {
      name: "lowestPrice",
      displayName: "Lowest Price",
      type: "string",
      role: "value",
    },
    highestPrice: {
      name: "highestPrice",
      displayName: "Highest Price",
      type: "string",
      role: "value",
    },
  },
  methods: {
  },
  dataSources: {
  },
}
export const Log = domain.types.Log = {
  name: "Log",
  displayName: "Log",
  get displayProp() { return this.props.id }, 
  type: "model",
  controllerRoute: "Log",
  get keyProp() { return this.props.id }, 
  behaviorFlags: 0,
  props: {
    id: {
      name: "id",
      displayName: "Id",
      type: "number",
      role: "primaryKey",
      hidden: 3,
    },
    application: {
      name: "application",
      displayName: "Application",
      type: "string",
      role: "value",
    },
    date: {
      name: "date",
      displayName: "Date",
      type: "string",
      role: "value",
    },
    level: {
      name: "level",
      displayName: "Level",
      type: "string",
      role: "value",
    },
    message: {
      name: "message",
      displayName: "Message",
      type: "string",
      role: "value",
    },
    callSite: {
      name: "callSite",
      displayName: "Call Site",
      type: "string",
      role: "value",
    },
    exception: {
      name: "exception",
      displayName: "Exception",
      type: "string",
      role: "value",
    },
    user: {
      name: "user",
      displayName: "User",
      type: "string",
      role: "value",
    },
    url: {
      name: "url",
      displayName: "Url",
      type: "string",
      role: "value",
    },
    urlReferrer: {
      name: "urlReferrer",
      displayName: "Url Referrer",
      type: "string",
      role: "value",
    },
    browser: {
      name: "browser",
      displayName: "Browser",
      type: "string",
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
export const SubscriptionFeature = domain.types.SubscriptionFeature = {
  name: "SubscriptionFeature",
  displayName: "Subscription Feature",
  get displayProp() { return this.props.name }, 
  type: "model",
  controllerRoute: "SubscriptionFeature",
  get keyProp() { return this.props.id }, 
  behaviorFlags: 7,
  props: {
    id: {
      name: "id",
      displayName: "Id",
      type: "number",
      role: "primaryKey",
      hidden: 3,
    },
    name: {
      name: "name",
      displayName: "Name",
      type: "string",
      role: "value",
      rules: {
        required: val => (val != null && val !== '') || "Name is required.",
      }
    },
    isEnabled: {
      name: "isEnabled",
      displayName: "Is Enabled",
      type: "boolean",
      role: "value",
    },
    subscriptionLevels: {
      name: "subscriptionLevels",
      displayName: "Subscription Levels",
      type: "collection",
      itemType: {
        name: "$collectionItem",
        displayName: "",
        role: "value",
        type: "model",
        get typeDef() { return (domain.types.SubscriptionLevel as ModelType) },
      },
      role: "value",
      dontSerialize: true,
    },
  },
  methods: {
  },
  dataSources: {
  },
}
export const SubscriptionLevel = domain.types.SubscriptionLevel = {
  name: "SubscriptionLevel",
  displayName: "Subscription Level",
  get displayProp() { return this.props.id }, 
  type: "model",
  controllerRoute: "SubscriptionLevel",
  get keyProp() { return this.props.id }, 
  behaviorFlags: 7,
  props: {
    id: {
      name: "id",
      displayName: "Id",
      type: "number",
      role: "primaryKey",
      hidden: 3,
    },
    subscriptionName: {
      name: "subscriptionName",
      displayName: "Subscription Name",
      type: "string",
      role: "value",
      rules: {
        required: val => (val != null && val !== '') || "Subscription Name is required.",
      }
    },
    features: {
      name: "features",
      displayName: "Features",
      type: "collection",
      itemType: {
        name: "$collectionItem",
        displayName: "",
        role: "value",
        type: "model",
        get typeDef() { return (domain.types.SubscriptionFeature as ModelType) },
      },
      role: "value",
      dontSerialize: true,
    },
  },
  methods: {
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
    subscriptionName: {
      name: "subscriptionName",
      displayName: "Subscription Name",
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
    activeFeatures: {
      name: "activeFeatures",
      displayName: "Active Features",
      type: "collection",
      itemType: {
        name: "$collectionItem",
        displayName: "",
        role: "value",
        type: "string",
      },
      role: "value",
    },
    inactiveFeatures: {
      name: "inactiveFeatures",
      displayName: "Inactive Features",
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
    confirmEmail: {
      name: "confirmEmail",
      displayName: "Confirm Email",
      transportType: "item",
      httpMethod: "POST",
      params: {
        userId: {
          name: "userId",
          displayName: "User Id",
          type: "string",
          role: "value",
        },
        confirmationToken: {
          name: "confirmationToken",
          displayName: "Confirmation Token",
          type: "string",
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
}

interface AppDomain extends Domain {
  enums: {
  }
  types: {
    ApplicationUser: typeof ApplicationUser
    Container: typeof Container
    Item: typeof Item
    ItemBarcodeInformation: typeof ItemBarcodeInformation
    Log: typeof Log
    RestrictedAccessCode: typeof RestrictedAccessCode
    SiteInfoDto: typeof SiteInfoDto
    SubscriptionFeature: typeof SubscriptionFeature
    SubscriptionLevel: typeof SubscriptionLevel
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
