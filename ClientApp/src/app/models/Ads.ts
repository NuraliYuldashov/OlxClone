import { SubCategory } from "./SubCategory";
import { SubRegion } from "./SubRegion";
import { User } from "./User";

export interface Ads {
    Id: number;
    UserId: string;
    Title: string;
    Description: string;
    SubCategoryId: number;
    Price: number;
    SubRegionId: number;
    State: string;
    ImageUrls: string[];
    SubCategoryDto: SubCategory,
    SubRegionDto: SubRegion,
    User: User
}