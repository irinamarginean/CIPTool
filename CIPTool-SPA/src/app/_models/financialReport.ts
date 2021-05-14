export interface FinancialReport {
  id: string;
  plannedSavings: number;
  plannedExpenses: number;
  plannedBalance: number;
  actualSavings: number;
  actualExpenses: number;
  actualBalance: number;
  bonus: number;
  uploadedAt?: Date;
  modifiedAt?: Date;
}
