export abstract class SessionStorage {
  readonly length: number=0;
  abstract clear(): void;
  abstract getItem(key: string): string | null;
  abstract key(index: number): string | null;
  abstract removeItem(key: string): void;
  abstract setItem(key: string, data: string): void;
  [key: string]: any;
  [index: number]: string;
}
