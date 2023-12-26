import { User } from "../user/user.model";

export interface Task {
    taskId: string;
    title: string;
    description: string;
    dueDate: Date;
    createdAt: Date;
    updatedAt: Date;
    isActive: boolean;
    taskOwnerId: string;
    user: User; // Assuming you have a User model
  }
