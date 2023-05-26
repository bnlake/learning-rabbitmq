import axios from "axios";
import Worker from "../models/Worker";

export default class WorkersClient {
	constructor(public basepath: string) {}

	async GetAll(abortSignal: AbortSignal): Promise<Array<Worker>> {
		const uri = new URL(this.basepath + "/workers");

		const result = (await axios
			.get(uri.toString(), { signal: abortSignal })
			.then((result) => result.data)) as Array<Worker>;

		return result;
	}
}
