import { LegacyRef, forwardRef, useEffect, useState } from "react";
import useContentAssignmentClient from "../hooks/ContentAssignmentClientHook";
import Content from "../models/Content";

const ContentList = forwardRef((_, ref: LegacyRef<HTMLSelectElement>) => {
	const client = useContentAssignmentClient();
	const [content, setContent] = useState<Array<Content>>([]);

	useEffect(() => {
		client.GetAllContent().then((c) => setContent(() => c));
		return () => setContent(() => []);
	}, [client]);

	return (
		<div style={{ display: "flex", flexDirection: "column" }}>
			<label htmlFor="selectedContent">Available Content</label>
			<select ref={ref} id="selectedContent" name="selectedContent">
				{content.map((c) => (
					<option key={c.id} value={c.id}>
						{c.title}
					</option>
				))}
			</select>
		</div>
	);
});

export default ContentList;
