import { NsfwSpyResult } from "./NsfwSpyResult"

export interface NsfwSpyFramesResult {
    frames: { [frame: number]: NsfwSpyResult }
    frameCount: number
    isNsfw: boolean
}