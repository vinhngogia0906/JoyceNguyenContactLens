import { UUID } from "crypto";

export interface ContactLens {
  id: UUID;
  name: string;
  color: string;
  degree: number;
  price: number;
  quantity: number;
}