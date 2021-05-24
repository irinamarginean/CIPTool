import { AttachmentDetails } from './attachmentDetails';
import { AddIdeaInfoDto } from './addIdeaInfoDto';
import { FinancialReport } from './financialReport';
import { LeaderResponseDetails } from './leaderResponseDetails';

export interface IdeaDetails {
  id: string;
  title: string;
  description: string;
  richTextDescription: string;
  status: number;
  context: string;
  target: string;
  reviewerName: string;
  ideaNumber: string;
  planDate?: Date;
  doDate?: Date;
  modifiedAt?: Date;
  leaderResponseAt?: Date;
  checkDate?: Date;
  actDate?: Date;
  isAssociateResponsible: boolean;
  isIdeaSavingMoney: boolean;
  ideaOwnerDetails: AddIdeaInfoDto;
  financialReport: FinancialReport;
  categories: Array<string>;
  attachments: Array<AttachmentDetails>;
  leaderResponses: Array<LeaderResponseDetails>;
}
