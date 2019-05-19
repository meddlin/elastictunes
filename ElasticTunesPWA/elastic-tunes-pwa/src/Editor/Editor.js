import React from 'react';
import { withFormik, Form, Field } from 'formik';
import * as Yup from 'yup';

import { DefaultButton } from 'office-ui-fabric-react/lib/Button';
import { Label } from 'office-ui-fabric-react/lib/Label';
import { TextField } from 'office-ui-fabric-react/lib/TextField';

const EditorFormik = props => {
    const {
		values,
		touched,
		errors,
		handleChange,
		handleBlur,
		handleSubmit,
    } = props;
    
    return (
        <Form>
            <Label>Editing a song in the library...</Label>

            <TextField 
                name="songName"
                label="Song Name"
                onChange={handleChange}
				onBlur={handleBlur}
				value={values.songName}
            />

            <TextField 
                name="artist"
                label="Artist"
                onChange={handleChange}
				onBlur={handleBlur}
				value={values.artist}
            />

            <TextField 
                name="genre"
                label="Genre"
                onChange={handleChange}
				onBlur={handleBlur}
				value={values.genre}
            />

            <DefaultButton type="submit" text="Save" />
        </Form>
    );
};

export const Editor = withFormik({
    mapPropsToValues({ songName, artist, genre }) {
        return {
            songName: songName || '',
            artist: artist || '',
            genre: genre || ''
        }
    },
    validationSchema: Yup.object().shape({
        songName: Yup.string().required('Song name is required.'),
        artist: Yup.string().required('Artist is required.')
    }),
    handleSubmit(values, { props, setSubmitting }) {
        console.log(values);

        setSubmitting(false);
    }
})(EditorFormik);