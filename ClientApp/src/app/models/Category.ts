import { SubCategory } from "./SubCategory";

export interface Category {
    Id: number;
    Name: string;
    ImageUrl: string;
    SubCategories: SubCategory[];
}