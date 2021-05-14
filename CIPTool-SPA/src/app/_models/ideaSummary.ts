import { FinancialReportSummary } from './financialReportSummary';

export interface IdeaSummary {
  id: string;
  title: string;
  status: number;
  planDate?: Date;
  modifiedAt?: Date;
  leaderResponseAt?: Date;
  actualImplementationDate?: Date;
  associateName?: string;
  reviewerName: string;
  financialReport: FinancialReportSummary;
  categories: Array<string>;
}
