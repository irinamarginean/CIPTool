import { Attachment } from './attachment';
import { FinancialReport } from './financialReport';

export interface EditIdeaDto {
  title: string;
  description: string;
  richTextDescription: string;
  context: string;
  target: string;
  doDate?: Date;
  checkDate?: Date;
  actDate?: Date;
  modifiedAt?: Date;
  isAssociateResponsible: boolean;
  financialReport: FinancialReport;
  categories: Array<string>;
  attachments: Array<Attachment>;
}
