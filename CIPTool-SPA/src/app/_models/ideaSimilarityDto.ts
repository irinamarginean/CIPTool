export interface IdeaSimilarityDto {
  id: string;
  title: string;
  description: string;
  context: string;
  target: string;
  similarity: number;
}
