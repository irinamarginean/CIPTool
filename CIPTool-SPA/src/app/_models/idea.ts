import { UserOverviewDto } from './userOverviewDto';
import { Attachment } from './attachment';
import { FinancialReport } from './financialReport';

export interface Idea {
  id: string;
  title: string;
  description: string;
  richTextDescription: string;
  status: number;
  context: string;
  target: string;
  planDate?: Date;
  doDate?: Date;
  checkDate?: Date;
  actDate?: Date;
  modifiedAt?: Date;
  plannedImplementationDate?: Date;
  actualStartImplementationDate?: Date;
  actualImplementationDate?: Date;
  isAssociateResponsible: boolean;
  isIdeaSavingMoney: boolean;
  associateName: string;
  implementationResponsible: UserOverviewDto;
  financialReport: FinancialReport;
  categories: Array<string>;
  attachments: Array<Attachment>;
}
