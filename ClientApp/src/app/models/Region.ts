import { SubRegion } from "./SubRegion";

export interface Region {
    Id: number;
    Name: string;
    SubRegions: SubRegion[];
}