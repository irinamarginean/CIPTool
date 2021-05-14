import { IdeaSummary } from './ideaSummary';

export interface LeaderResponseOverview {
  waitingForApprovalIdeasOverview: Array<IdeaSummary>;
  leaderResponses: Array<IdeaSummary>;
}
