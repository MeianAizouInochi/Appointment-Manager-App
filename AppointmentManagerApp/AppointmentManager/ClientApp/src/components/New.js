export default function New(props)
{
    return (
        <div className="model-container">
            <div className="model-title">
                New Component
            </div>

            <div className="mt-15">
                <label htmlFor="Title_n">Title</label> <br />
                <input type="text" id="Title_n" maxLength={150} name="title" />
                <span className="ms-10">0/150</span>
            </div>

            <div className="mt-15">
                <label htmlFor="Description_n">Description</label> <br />
                <input type="text" id="Description_n" maxLength={300} name="description" />
                <span>0/300</span>
            </div>

            <div className="disp mt-15">
                <div>
                    <label htmlFor="Address_n">Address</label>
                    <input type="text" id="Address_n" name="address" maxLength={100}/>
                </div>

                <div className="mt-10">
                    <label htmlFor="LevelOfImportance_n">Importance</label>
                    <select id="LevelOfImportance_n" name="levelofimportance" defaultValue={2}>

                        <option value={5}>
                            Very High
                        </option>

                        <option value={4}>
                            High
                        </option>

                        <option value={3}>
                            Medium
                        </option>

                        <option value={2}>
                            Normal
                        </option>

                        <option value={1}>
                            Low
                        </option>

                        <option value={0}>
                            Very Low
                        </option>

                    </select>
                </div>

            </div>

        </div>
    )
}