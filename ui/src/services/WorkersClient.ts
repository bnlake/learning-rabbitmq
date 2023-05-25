import Worker from "../models/Worker";

export default class WorkersClient {
	constructor(public basepath: string) {}

	async GetAll(): Promise<Array<Worker>> {
		const uri = new URL(this.basepath + "workers");

		const result = await fetch(uri);
		const objects = (await result.json()) as Array<Worker>;

		return objects;
	}
}
