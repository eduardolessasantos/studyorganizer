export interface Subtopic {
    Id: number;
    Description: string;
    DisciplineId: number;
    Status: number;
    StartDate?: string | null;
    EndDate?: string | null;
    Notes?: string | null;
    MaterialUrl?: string | null;
    MasteryLevel?: number | null;
    Content?: string | null;
}

export interface Discipline {
    Id: number;
    Name: string;
    ModuleId: number;
    Subtopics: Subtopic[];
}