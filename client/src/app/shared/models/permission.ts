export interface Permission {
    roleId: number
    name?: string
    description?: string
    isDefault?: boolean
    permissions: string[]
}

export interface PermissionNode {
  name: string;
  children: PermissionNode[] | null;
  defaultChecked: boolean;
}

export interface PermissionFlatNode {
  expandable: boolean;
  name: string;
  level: number;
  defaultChecked: boolean;
}
