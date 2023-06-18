export default class Content {
	constructor(public title: string, public id = crypto.randomUUID()) {}
}
