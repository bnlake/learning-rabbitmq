import axios from "axios";
import Content from "../models/Content";

export default class ContentAssignmentClient {
	constructor(public basepath: string) {}

	async GetAllContent(): Promise<Array<Content>> {
		const uri = new URL(this.basepath + "/content");

		const result = (await axios
			.get(uri.toString())
			.then((result) => result.data)) as Array<Content>;

		return result;
	}

	async AssignContent(contentId: string, patientId: string) {
		const url = new URL(this.basepath + "/assign");

		await axios.post(url.toString(), { patientId, contentId });
	}
}
