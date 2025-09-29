export interface Discipline {
    Id: number;
    Name: string;
    ModuleId: number;
}

export interface Module {
    Id: number;
    Name: string;
    Disciplines: Discipline[];
}