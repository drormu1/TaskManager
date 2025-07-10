export interface ManagedTaskDto {
  id: number;
  title: string;
  description: string;
  taskTypeId: number;
  taskTypeName: string;
  taskStatusId: number;
  taskStatusName: string;
  assignedUserId: number;
  assignedUserName: string;
  createdAt: string;
  closedAt?: string;
  isClosed: boolean;
  taskDataJson?: string;
}