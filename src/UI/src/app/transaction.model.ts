export interface Transaction  {    
  id: string;  
  uuid?: string;  
  Type: string;
  Amount: number;
  EffectiveDate: Date;
  
  showAll: boolean;
}
