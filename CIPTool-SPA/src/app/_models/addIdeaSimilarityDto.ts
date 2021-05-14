export interface AddIdeaSimilarityDto {
  title: string;
  description: string;
  context: string;
  target: string;
  categories: Array<string>
}
