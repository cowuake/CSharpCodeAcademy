import { ITenant } from "./ITenant"

export interface IBill {
    name?: string,
    firstDay: Date,
    lastDay: Date,
    amount: number,
    tenants: ITenant[]
}