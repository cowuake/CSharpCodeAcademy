export interface ITenant {
    firstName: string,
    lastName?: string,
    firstDate?: Date,
    lastDate?: Date,
    daysOut?: number,
    dayOfBilling: number,
    dueAmount: number
}