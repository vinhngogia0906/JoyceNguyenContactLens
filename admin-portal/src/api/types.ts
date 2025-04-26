import { UUID } from "crypto";

export interface ContactLens {
  id: UUID;
  name: string;
  color: string;
  degree: number;
  price: number;
  quantity: number;
  images?: Array<{
    id: string;
    url: string;
    isThumbnail?: boolean;
  }>;
}

export interface Order {
  id: UUID,
  orderDate: Date,
  totalPrice: number
}

export interface ProductUpdateInput {
  name: string;
  color: string;
  degree: number;
  price: number;
  quantity: number;
}