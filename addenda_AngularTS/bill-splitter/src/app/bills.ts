import { IBill } from './IBill';

export const BILLS: IBill[] = [
  {
    name: 'Test bill',
    firstDay: new Date(2000, 1, 1),
    lastDay: new Date(2030, 12, 31),
    amount: 1000.0,
    tenants: [
      {
        firstName: 'Hello',
        lastName: 'World',
        dayOfBilling: 1000,
        dueAmount: 1000.0,
      },
    ],
  },
];
