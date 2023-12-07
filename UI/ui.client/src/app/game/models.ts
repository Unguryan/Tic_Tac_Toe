export interface Game {
    id: number;
    boards: Board[][];
    boardToPlay: BoardCoords | null;
    playerToMove: PlayerType;
    winner: Winner | null;
}

export interface Board {
    winner: Winner | null;
    boardCoords: BoardCoords | null;
    selected: boolean;
    cells: Cell[][];
}

export interface BoardCoords {
    x: number;
    y: number;
}

export enum PlayerType {
    White = 1,
    Black = 2
}

export enum Winner {
    White = 1,
    Black = 2,
    Draw = 3
}

export interface Cell {
    x: number;
    y: number;
    value: PlayerType | null;
}