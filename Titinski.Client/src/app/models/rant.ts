import { Resource } from "./Resource";

export interface Rant extends Resource {
    description: string;
    path: string;
}
