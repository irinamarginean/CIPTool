import { FinancialStatisticsDto } from './financialStatisticsDto';

export interface IdeaStatisticsDto {
  allIdeasNumber: number;
  waitingForApprovalIdeasNumber: number;
  approvedIdeasNumber: number;
  postponedIdeasNumber: number;
  declinedIdeasNumber: number;
  implementedIdeasNumber: number;
  otdDecisionValue: number;
  otdImplementationValue: number;
  otdDecisionGreenCategoryNumber: number;
  otdDecisionYellowCategoryNumber: number;
  otdDecisionRedCategoryNumber: number;
  otdImplementationGreenCategoryNumber: number;
  otdImplementationYellowCategoryNumber: number;
  otdImplementationRedCategoryNumber: number;
  savingsValues: FinancialStatisticsDto;
  expensesValues: FinancialStatisticsDto;
  balanceValues: FinancialStatisticsDto;
  bonusValues: FinancialStatisticsDto;
  financialBenefitsIdeasNumber: number;
  noFinancialBenefitsIdeasNumber: number;
  totalMoneySaved: number;
  totalBonuses: number;
}
