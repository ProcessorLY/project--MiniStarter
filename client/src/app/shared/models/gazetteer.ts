export interface Province {
  type: string;
  code: number;
  nameKH: string;
  nameEN: string;
}

export interface District {
  type: string | null;
  code: number | null;
  provinceCode: number;
  nameKH: string | null;
  nameEN: string | null;
}

export interface Commune {
  type: string | null;
  code: number | null;
  districtCode: number;
  nameKH: string | null;
  nameEN: string | null;
}

export interface Village {
  type: string | null;
  code: number | null;
  communeCode: number | null;
  nameKH: string | null;
  nameEN: string | null;
}
